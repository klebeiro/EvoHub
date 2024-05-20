import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GitHubApiService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  list() {
    return this.http.get(this.baseUrl + "githubapi/get-repositories").subscribe(x => console.log(x));
  }

}
