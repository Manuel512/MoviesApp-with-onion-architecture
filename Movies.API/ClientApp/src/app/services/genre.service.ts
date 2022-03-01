import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Genre } from '../Models/genre';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  private uri: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.uri = baseUrl + "api/Genres";
  }

  getAllGenres(): Observable<Genre[]> {
    return this.http.get<Genre[]>(this.uri);
  }
}
