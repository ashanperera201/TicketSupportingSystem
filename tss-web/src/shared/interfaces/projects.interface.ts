export interface IProject {
    id: string;
    projectAssignedUser: string;
    projectName: string,
    projectCode: string,
    status: number,
    createdBy: string,
    createdOn: Date,
    lastModifiedBy: string,
    lastModifiedOn: Date,
    isDeleted: boolean,
    user: any
}