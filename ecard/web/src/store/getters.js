const getters = {
    sidebar: state => state.app.sidebar,
    avatar: state => state.user.avatar,
    name: state => state.user.name,
    roles: state => state.user.roles,
    modules: state => state.user.modules,
    moduleAuths: state => state.user.moduleAuths,
    moduleList: state => state.user.moduleList
}
export default getters