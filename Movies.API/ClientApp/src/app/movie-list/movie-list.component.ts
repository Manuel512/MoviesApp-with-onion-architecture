import { Component, OnInit } from '@angular/core';
import { Movie } from '../Models/movie';
import { MovieService } from '../services/movie.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit {

  movies: Movie[];

  constructor(private movieService: MovieService, private router: Router) { }

  ngOnInit() {
    this.movieService.getAllMovies().subscribe((data: Movie[]) => {
      this.movies = data;
    });
  }

  addMovie() {
    this.router.navigate(['AddMovie']);
  }

  viewDetail(id:number) {
    this.router.navigate(['DetailMovie/' + id]);
  }
}
