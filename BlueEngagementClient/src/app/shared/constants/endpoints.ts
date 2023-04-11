export const ENDPOINTS_MAP = {
    AUTHENTICATION: {
        GET_CURRENT_USER: 'account',
        LOGIN: 'account/login',
        REGISTER: 'account/register',
    },

    USER: {
        GET_LEADERBOARD: 'user/getLeaderboard',
    },

    QUEST: {
        GET_ALL_QUESTS: 'quest/GetAllQuests',
        GET_QUEST_DETAILS: 'quest/GetQuestDetails/',
        CREATE_QUEST: 'quest/postNewQuest',
        GET_ALL_QUESTS_FROM_USER: 'quest/GetQuestsFromUser',
        GET_LOGGED_IN_USERS_QUESTS: 'quest/GetLoggedInUsersQuests',
    },

    TAKEN_QUEST: {
        ACCEPT_QUEST: 'takenQuest/acceptQuest/',
        COMPLETE_QUEST: 'takenQuest/completeTakenQuest/',
        GET_ALL_ACCEPTED_QUESTS_FOR_USER:
            'takenQuest/getAllAcceptedQuestsForUser',
        GET_ALL_COMPLETED_QUESTS_FOR_USER:
            'takenQuest/getAllCompletedQuestsForLoggedInUser',
    },
};
