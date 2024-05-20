import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { ActionResult } from '../../models/ActionResult';
import { Repository } from '../../models/Repository';

@Injectable({
  providedIn: 'root'
})
export class GithubApiService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  private mapApiResultToRepository(apiResult: ActionResult<Repository[]>) {
    return apiResult.result.map(r => ({
      id: r.id,
      name: r.name,
      owner: {
        login: r.owner.login
      },
      description: r.description,
      language: r.language,
      updatedAt: r.updatedAt,
      url: r.url,
    }))
  }

  listRepositories() {

    const result = this.http.get<ActionResult<Repository[]>>(`${this.baseUrl}githubapi/repositories`);

    return result.pipe(map(this.mapApiResultToRepository))
  }

  getRepository(owner: string, id: number) {
    const result = this.http.get<ActionResult<Repository[]>>(`${this.baseUrl}githubapi/owner-repository-by-id?owner=${owner}&id=${id}`);

    return result.pipe(map(this.mapApiResultToRepository))
  }

  listRepositoriesByName(name: string) {
    const result = this.http.get<ActionResult<Repository[]>>(`${this.baseUrl}githubapi/repositories-by-name?name=${name}`);

    return result.pipe(map(this.mapApiResultToRepository))
  }
}
