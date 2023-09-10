import { IAuthLogin } from "../interfaces/auth-login.interface";
import httpApiKit, { baseUrl } from '../services/axios-http-kit';

export const authLogin = (loginPayload: IAuthLogin): Promise<any> => {
    const apiVersion = process.env.REACT_APP_TSS_BACKEND_VERSION;
    const apiUrl: string = `${baseUrl}/${apiVersion}/users/auth/login`;
    return httpApiKit.post(apiUrl, loginPayload)
}

export const getUser = (userId: string): Promise<any> => {
    const apiVersion = process.env.REACT_APP_TSS_BACKEND_VERSION;
    const apiUrl: string = `${baseUrl}/${apiVersion}/users/${userId}`;
    return httpApiKit.get(apiUrl);
}