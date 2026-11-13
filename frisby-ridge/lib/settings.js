require('../bin/exceptions.js').validate(
    {
        configs: process.env.configs,
        environment: process.env.environment
    }
);

var logger = require('../bin/logger.js');
var utils = require('../bin/utils.js');
var fw = require('../bin/frisbywrap.js');
var usertypes = require('../bin/usertypes.js').usertypes;

var template = function(type){
    return JSON.stringify({
        testcase: 'settings',
        description: 'Changing ' + type + ' setting {1} to {2}',
        usertype: usertypes.superuser,
        method: 'PUT',
        route: utils.format('/' + type + '/{0}/settings/{1}'), 
        data: { 
            value: '{2}'
        },    
        status: 200, 
        response: {}
    });
}

var tests = function(type, args){
    var result = [];

    for(var i = 0, l = args.length; i < l; i++){
        var arg = args[i];
        result.push(JSON.parse(utils.format(template(type), arg.serviceGuid, arg.name, arg.value)));
    }

    return result;
}

exports.build = function(servicesettings, collectionsettings, callback){
    var configs = process.env.configs;

    return require('../bin/login.js').build(function(context){
        fw.loop(tests('services', servicesettings).concat(tests('collections', collectionsettings)), context, function(output){                
            fw.resetenvironment(configs);
            (process.env.anonymous == "undefined") ? fw.run(require('../bin/login.js').build(callback)) : callback();
        });
    }, '/authenticate', usertypes.superuser, '../DirectoryAPI/harness_configs.js');
};
