import { Component, OnInit } from '@angular/core';
import { Repository } from '../../models/Repository';
import { GithubApiService } from '../../services/github-api/github-api.service';

@Component({
  selector: 'app-other-repositories',
  templateUrl: './other-repositories.component.html',
  styleUrls: ['./other-repositories.component.css']
})
export class OtherRepositoriesComponent {
  repositories: Repository[] = [];
  queryString: string = "";
  isInputValid: boolean = true;
  isLoading: boolean = false;
  hasLoaded: boolean = false;


  public search() {

    if (this.queryString == "") {
      this.isInputValid = false;
      return;
    }

    this.repositories = [];
    this.isInputValid = true;
    this.isLoading = true;
    this.hasLoaded = true;

    this.gitHubApiService.listRepositoriesByName(this.queryString).subscribe(result => {
      this.repositories = result
      this.isLoading = false;
    });
  }

  constructor(private gitHubApiService: GithubApiService) { }

}
