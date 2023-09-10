import httpApiKit, { baseUrl } from '../services/axios-http-kit';

export const fetchProjects = (): Promise<any> => {
    const apiVersion = process.env.REACT_APP_TSS_BACKEND_VERSION;
    const apiUrl: string = `${baseUrl}/${apiVersion}/projects`;
    return httpApiKit.get(apiUrl);
}