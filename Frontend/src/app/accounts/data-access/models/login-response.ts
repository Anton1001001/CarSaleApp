export interface LoginResponse {
    userId: string,
    email: string | null
    name: string,
    accessToken: string
}