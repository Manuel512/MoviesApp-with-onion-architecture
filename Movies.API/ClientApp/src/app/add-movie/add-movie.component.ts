import { Component, OnInit } from '@angular/core';
import { MovieService } from '../services/movie.service';
import { GenreService } from '../services/genre.service';
import { Router } from '@angular/router';
import { Movie } from '../Models/movie';
import { Genre } from '../Models/genre';

declare var $: any;

@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.css']
})
export class AddMovieComponent implements OnInit {

  movie: Movie;
  genres: Genre[];
  genreSelected: Genre;
  saving: boolean = false;

  constructor(private movieService: MovieService,
    private genreService: GenreService,
    private router: Router) {
    this.movie = new Movie();
    this.movie.genres = [];
  }

  ngOnInit() {
    this.genreService.getAllGenres().subscribe((data: Genre[]) => {
      this.genres = data;
    });
  }

  goBack() {
    $("#saveModal, #goBackModal").modal('hide');
    this.router.navigate(['']);
  }

  saveMovie() {
    console.log(this.movie);
    $("#saveModal").modal('show');
  }

  cancelSave() {
    $("#saveModal").modal('hide');
  }

  confirmSave() {
    this.saving = true;
    this.movieService.addMovie(this.movie).subscribe(() => {
      $("#saveModal").modal('hide');
      $("#goBackModal").modal('show');
    }, error => {
      this.saving = false;
      alert(error);
    });
  }

  addGenre() {
    if (this.genreSelected) {
      this.movie.genres.push(Object.assign({}, this.genreSelected));
      this.genreSelected = null;
    }
  }

  removeGenre(id: number) {
    const index = this.movie.genres.findIndex(x => x.id == id);
    if (index > -1) this.movie.genres.splice(index, 1);
  }
}
