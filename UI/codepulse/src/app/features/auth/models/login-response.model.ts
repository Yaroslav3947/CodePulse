export interface LoginResponse {
    userId: string,
    token: string;
    email: string;
    roles: string[];
}