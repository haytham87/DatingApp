import { Photos } from './Photos';

export interface User {
    userID: number;
    userName: string;
    knowAs: string;
    age: number;
    gender: string;
    createdDate: Date;
    lastActive: Date;
    photoUrl?: string;
    city: string;
    country: string;
    interests?: string;
    intoduction?: string;
    lookingFor?: string;
    photos?: Photos[];
}
