import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppRoutingApi } from 'src/app/app-routing-api';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  getProfile() {
    return this.http.get(AppRoutingApi.GetProfile);
  }

  updateProfile(user: User) {
    return this.http.put(AppRoutingApi.UpdateProfile, user);
  }

  getUsersLikeMe() {
    return this.http.get(AppRoutingApi.UsersLikeMe);
  }

  sendLike(recipientId: string) {
    return this.http.post(AppRoutingApi.LikeUser, recipientId);
  }

  getUsers() {
    return this.http.get(AppRoutingApi.GetUsers);
  }

  getUsersById() {
    return this.http.get(AppRoutingApi.GetUsers);
  }
}
