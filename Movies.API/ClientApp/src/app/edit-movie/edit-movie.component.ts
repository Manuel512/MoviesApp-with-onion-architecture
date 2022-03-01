import { Component, OnInit } from '@angular/core';
import { Movie } from '../Models/movie';
import { Genre } from '../Models/genre';
import { GenreService } from '../services/genre.service';
import { MovieService } from '../services/movie.service';
import { Router, ActivatedRoute } from '@angular/router';

declare var $: any;

@Component({
  selector: 'app-edit-movie',
  templateUrl: './edit-movie.component.html',
  styleUrls: ['./edit-movie.component.css']
})
export class EditMovieComponent implements OnInit {

  movie: Movie;
  movieId: number;
  genres: Genre[];
  genreSelected: Genre;
  saving: boolean = false;

  constructor(private movieService: MovieService,
    private genreService: GenreService,
    private route: ActivatedRoute,
    private router: Router) {
    this.movie = new Movie();
    this.movie.genres = [];
  }

  ngOnInit() {
    this.movieId = Number(this.route.snapshot.paramMap.get('id'));
    this.movieService.getMovie(this.movieId).subscribe((data: Movie) => {
      this.movie = data;
      this.movie.releaseDate = new Date(this.movie.releaseDate);
    });

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
    this.movieService.updateMovie(this.movieId, this.movie).subscribe(() => {
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
