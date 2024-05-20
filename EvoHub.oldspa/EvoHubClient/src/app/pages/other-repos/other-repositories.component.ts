import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Repository } from '../../models/Repository';

@Component({
  selector: 'app-other-repositories',
  templateUrl: './other-repositories.component.html',
  styleUrls: ['./other-repositories.component.css']
})
export class OtherRepositoriesComponent {
  private _http:HttpClient
  private _baseUrl:string
  public repositories: Repository[] = [];
  public queryString: string = "";
  public isInputValid: boolean = true;
  public hasSearchedRepository = false;


  public search() {
    if (this.queryString == "") {
      this.isInputValid = false;
      return;
    }
    this.repositories = [];
    this.isInputValid = true;
    this.hasSearchedRepository = true;

    this._http.get<Repository[]>(this._baseUrl + `repo/search?searchterm=${this.queryString.replace(" ", "+")}`).subscribe(result => {
      this.repositories = result;
    }, error => console.error(error));
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._http = http;
    this._baseUrl = baseUrl;
  }
}
