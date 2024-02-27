export interface User {
    id: string;
    email: string;
    username: string;
    phonenumber: string | null;
    twoFactorEnabled: string;
}