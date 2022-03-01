import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Movie } from '../Models/movie';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  private uri: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.uri = baseUrl + "api/Movies";
  }

  getAllMovies():Observable<Movie[]> {
    return this.http.get<Movie[]>(this.uri);
  }

  getMovie(id:number): Observable<Movie> {
    return this.http.get<Movie>(this.uri + "/" + id);
  }

  addMovie(movie:Movie): Observable<Movie> {
    return this.http.post<Movie>(this.uri, movie);
  }

  updateMovie(id: number, movie: Movie): Observable<{}> {
    return this.http.put<Movie>(this.uri + "/" + id, movie);
  }

  deleteMovie(id: number): Observable<{}> {
    return this.http.delete<Movie>(this.uri + "/" + id);
  }
}
