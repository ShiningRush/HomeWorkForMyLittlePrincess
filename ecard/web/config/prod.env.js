var server_ip = '';

module.exports = {
  NODE_ENV: '"production"',
  SERVER_IP: JSON.stringify(server_ip),
  BASE_API: JSON.stringify(server_ip + '/api'),
  SUB_API: JSON.stringify(server_ip + '/subapi')
}