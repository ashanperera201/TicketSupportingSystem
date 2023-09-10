import jwt_decode from "jwt-decode";

export const setAccessToken = (token: string): void => {
    localStorage.setItem('access_token', token);
}

export const getAccessToken = (): string | null => {
    const token: string | null = localStorage.getItem('access_token');
    return token
}

export const decodeAccessToken = (): any | null => {
    const token = getAccessToken();
    if (token) {
        const decodedResult = jwt_decode(token);
        return decodedResult;
    }
    return null;
}