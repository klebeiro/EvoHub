import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { createEmptyRepository, Repository } from '../../models/Repository';
import { FavoritesService } from '../../services/favorites/favorites.service';

@Component({
  selector: 'app-repository-list',
  templateUrl: './repository-list.component.html',
  styleUrls: ['./repository-list.component.css']
})
export class RepositoryListComponent {

  datepipe: DatePipe = new DatePipe('pt-BR');

  @Input() repositories: Repository[] = []
  @Input() favorites: Repository[] = []
  @Input() isLoading: boolean = false
  @Input() hasLoaded: boolean = false
  @Input() showFavorites: boolean = false

  isVisibleRepositoryDetails: boolean = false;
  selectedRepositoryIsFavorite: boolean = false;
  isDisabled: boolean = false;

  repositoryDetails: Repository = createEmptyRepository()
  formattedLastUpdate: string = "";

  saveFavorite(id: number) {
    this.isDisabled = true;
    this.favoritesService.saveFavorite(id).subscribe(updatedFavorites => {
      if (updatedFavorites.id == id) {
        let favoritedRepo = this.repositories.find(r => r.id == id)

        if (favoritedRepo != undefined) {
          this.favorites.push(favoritedRepo);
        }
      }

      this.checkIfSelectedRepositoryIsFavorite(id);
      this.isDisabled = false;
    }, () => { this.isDisabled = false });
  }

  deleteFavorite(id: number) {
    this.isDisabled = true;
    this.favoritesService.deleteFavorite(id).subscribe(updatedFavorites => {
      if (updatedFavorites.id == id) {
        const favoritedRepos = this.favorites.filter(r => r.id != id)

        this.favorites = favoritedRepos;
      }

      this.checkIfSelectedRepositoryIsFavorite(id);
      this.isDisabled = false;
    }, () => { this.isDisabled = false });
  }

  checkIfSelectedRepositoryIsFavorite(id: number) {
    const isFavorite = this.favorites.find(r => r.id == id)

    if (isFavorite != undefined) {
      this.selectedRepositoryIsFavorite = true;
    }
    else {
      this.selectedRepositoryIsFavorite = false;
    }
  }

  showRepositoryDetails(id: number) {
    let selectedRepository = this.repositories.find(r => r.id === id);

    if (selectedRepository == undefined) {
      selectedRepository = this.favorites.find(r => r.id === id);
    }

    if (selectedRepository != undefined) {

      const formattedLastUpdate = this.datepipe.transform(selectedRepository.updatedAt, 'dd-MMM-YYYY HH:mm:ss')
      if (formattedLastUpdate != null) {
        this.formattedLastUpdate = formattedLastUpdate
      }

      this.repositoryDetails = selectedRepository
      this.checkIfSelectedRepositoryIsFavorite(selectedRepository.id);

      this.isVisibleRepositoryDetails = true;
    }

  }

  hideRepositoryDetails() {
      this.isVisibleRepositoryDetails = false;
  }

  constructor(private favoritesService: FavoritesService) { }

}
