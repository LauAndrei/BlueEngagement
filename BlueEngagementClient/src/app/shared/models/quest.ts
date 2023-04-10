export interface IQuestCard {
    id: number;
    slug: string;
    title: string;
    description: string;
    reward: number;
    ownerUsername: string;
    datePosted: Date;
}

export interface IQuestDetails {
    id: number;

    title: string;

    description: string;

    reward: number;

    rewardsLeft: number;

    ownerUsername: string;

    numberOfCompletions: number;

    datePosted: Date;

    questStatus: string;
}
