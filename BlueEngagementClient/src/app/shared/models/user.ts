export interface ILoggedInUser {
    email: string;
    userName: string;
    score: number;
    token: string;
}

export interface ILeaderboardUser {
    userName: string;
    firstName: string;
    lastName: string;
    score: number;
}
