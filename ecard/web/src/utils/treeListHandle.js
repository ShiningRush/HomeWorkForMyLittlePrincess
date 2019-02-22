function buildChilds(oneNode, treeList, parentIdName, childPropName) {
    const childs = treeList.filter(p => p[parentIdName] == oneNode.id)
        //oneNode[childPropName] = childs
    if (childs.length > 0) {

        oneNode[childPropName] = childs
        for (var aChild of childs) {
            buildChilds(aChild, treeList, parentIdName, childPropName);
        }
    }
}

export function getTreeObj(treeList, parentIdName, childPropName) {
    if (treeList.length == 0)
        return []

    if (parentIdName === undefined)
        parentIdName = "parentId"

    if (childPropName === undefined)
        childPropName = "children"
    treeList.forEach(p => {
        if (treeList.filter(s => s.id == p.parentId).length == 0) {
            p.parentId = null;
        }
    })

    var nodeList = treeList.filter(p => p[parentIdName] == null)

    for (var aNode of nodeList) {
        buildChilds(aNode, treeList, parentIdName, childPropName);
    }

    return nodeList
}


export function getParent(treeList, nodeId) {
    if (treeList.length == 0)
        return [];
    var nodeList = [];
    if (nodeId == null) return null;
    var node = treeList.filter(p => p.id == nodeId)[0];
    if (node != null) {
        if (node.parentId) {
            getParent(treeList, node.parentId).forEach(element => {
                nodeList.push(element);
            });
        }
        nodeList.push(node.id);
    }
    return nodeList

}