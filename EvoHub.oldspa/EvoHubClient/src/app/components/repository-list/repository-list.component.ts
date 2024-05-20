import { Component, OnInit } from '@angular/core';
import { Repository } from '../../models/Repository';
import { GitHubApiService } from '../../services/githup-api.service';

@Component({
  selector: 'app-repository-list',
  templateUrl: './repository-list.component.html',
  styleUrls: ['./repository-list.component.css']
})
export class RepositoryListComponent implements OnInit {

  repositories: Repository[] = [];

  constructor(private gitHubApiService: GitHubApiService) { }

  ngOnInit() {
    this.gitHubApiService.list();
  }
}
