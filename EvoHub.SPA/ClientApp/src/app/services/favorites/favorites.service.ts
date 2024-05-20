import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { ActionResult } from '../../models/ActionResult';
import { Favorite } from '../../models/Favorite';
import { Repository } from '../../models/Repository';

@Injectable({
  providedIn: 'root'
})
export class FavoritesService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  private mapApiFavoriteResultListToRepository(apiResult: ActionResult<Favorite[]>): Repository[] {
    return apiResult.result.map(r => ({
      id: r.id,
      name: r.name,
      owner: { login: r.owner },
      description: r.description,
      language: r.language,
      updatedAt: r.updateLast,
      url: r.url,
    }));
  }

  private mapApiFavoriteResultSingleToRepository(apiResult: ActionResult<Favorite>): Repository {
    return ({
      id: apiResult.result.id,
      name: apiResult.result.name,
      owner: { login: apiResult.result.owner },
      description: apiResult.result.description,
      language: apiResult.result.language,
      updatedAt: apiResult.result.updateLast,
      url: apiResult.result.url,
    });
  }


  listFavorites() {
    const result = this.http.get<ActionResult<Favorite[]>>(`${this.baseUrl}favorites`);

    return result.pipe(map(this.mapApiFavoriteResultListToRepository))
  }

  saveFavorite(id: number) {
    const result = this.http.post<ActionResult<Favorite>>(`${this.baseUrl}favorites?id=${id}`, {});

    return result.pipe(map(this.mapApiFavoriteResultSingleToRepository))
  }

  deleteFavorite(id: number) {
    const result = this.http.delete<ActionResult<Favorite>>(`${this.baseUrl}favorites?id=${id}`);

    return result.pipe(map(this.mapApiFavoriteResultSingleToRepository))
  }
}
