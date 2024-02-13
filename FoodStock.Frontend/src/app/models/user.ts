export interface User {
    id?: string;
    email: string;
    password?: string;
    confirmPassword?:string;
    firstName: string;
    surname: string;
    roleId: string;
    roleName?:string;
}