export interface StartPageInfo {

    isStartPageEnabled: boolean;
    id: number;
    name: string;
    description: string;
    totalAttempts?: number;
    timeLimit?: number;
    totalQuestions?: number;
    passingScore?: number;
    
}