var merge = require('webpack-merge')
var prodEnv = require('./prod.env')

var server_ip = 'http://192.168.19.3:9090';

module.exports = merge(prodEnv, {
    NODE_ENV: '"development"',
    SERVER_IP: JSON.stringify(server_ip),
    BASE_API: JSON.stringify(server_ip + '/api'),
    SUB_API: JSON.stringify(server_ip + '/subapi')
})