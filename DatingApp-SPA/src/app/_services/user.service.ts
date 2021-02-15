import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';


@Injectable({
  providedIn: 'root'
})
export class UserService {
baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getUsers(): Observable<User[]>{
  return this.http.get<User[]>(this.baseUrl + 'users');
}

 getUser(userID): Observable<User> {
   return this.http.get<User>(this.baseUrl + 'users/' + userID);
 }

 updateUser(userID: number, user: User)
 {
 return this.http.put(this.baseUrl + 'users/' + userID, user);
 }

 setMainPhoto(userID: number, photoid: number){
  return this.http.post(this.baseUrl + 'users/' + userID + '/photos/' + photoid + '/setmain', {});
 }
}
