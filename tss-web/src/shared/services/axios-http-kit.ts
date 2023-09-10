import axios from 'axios';

export const baseUrl = process.env.REACT_APP_TSS_BACKEND

const httpApiKit = axios.create({
    baseURL: `${baseUrl}`,
    timeout: 1000
});

httpApiKit.interceptors.request.use(
    (config) => {
        config.headers.Authorization = `Bearer ${localStorage.getItem('access_token')}`;
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

httpApiKit.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        return Promise.reject(error);
    }
);

export default httpApiKit;