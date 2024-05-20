import { Component, OnInit } from '@angular/core';
import { Repository } from '../../models/Repository';
import { FavoritesService } from '../../services/favorites/favorites.service';
import { GithubApiService } from '../../services/github-api/github-api.service';

@Component({
  selector: 'app-my-repositories',
  templateUrl: './my-repositories.component.html',
  styleUrls: ['./my-repositories.component.css']
})
export class MyRepositoriesComponent implements OnInit {

  repositories: Repository[] = [];
  favorites: Repository[] = [];
  showFavorites: boolean = false;
  isLoadingRepositories: boolean = false;
  isLoadingFavorites: boolean = false;

  toggleShowFavorites() {
    this.showFavorites = !this.showFavorites;
  }

  constructor(private gitHubApiService: GithubApiService, private favoritesService: FavoritesService) { }

  ngOnInit(): void {
      this.isLoadingRepositories = true;
      this.isLoadingFavorites = true;
    
    this.gitHubApiService.listRepositories().subscribe(result => {
      this.repositories = result;
      this.isLoadingRepositories = false;
    }, (err) => this.isLoadingRepositories = false);

    this.favoritesService.listFavorites().subscribe(result => {
      this.favorites = result
      this.isLoadingFavorites = false;
    }, (err) => this.isLoadingFavorites = false);
  }

}
