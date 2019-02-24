import fetch from '@/utils/fetch'
import axios from 'axios'

export function getRobot(query)
{
    return fetch({
        url: '/robot/GetRobots',
        method: 'post',
        data: query
    })
}

export function updateRobot(robotInfo)
{
    return fetch({
        url: '/robot/InsertOrUpdateRobot',
        method: 'post',
        data: robotInfo
    })
}

export function deleteRobot(robotInfo)
{
    return fetch({
        url: '/robot/DeleteRobot',
        method: 'post',
        data: robotInfo
    })
}

// queryconditions: {
//     name: "string",
//     departmentId: "00000000-0000-0000-0000-000000000000",
//     orderBy: "string",
//     isAsc: true,
//     pageIndex: 1,
//     pageSize: 10
//   },
//   //弹窗实体
//   robotform: {
//     id: "string",
//     name: "string",
//     departmentId: "00000000-0000-0000-0000-000000000000",
//     description: "string",
//     isValid: true
// },