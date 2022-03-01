import { Component, OnInit } from '@angular/core';
import { MovieService } from '../services/movie.service';
import { Movie } from '../Models/movie';
import { Router, ActivatedRoute } from '@angular/router';

declare var $: any;

@Component({
  selector: 'app-movie-detail',
  templateUrl: './movie-detail.component.html',
  styleUrls: ['./movie-detail.component.css']
})
export class MovieDetailComponent implements OnInit {

  movie: Movie;
  movieId: number;
  deleting: boolean = false;

  constructor(private movieService: MovieService,
    private route: ActivatedRoute,
    private router: Router) {
    this.movie = new Movie();
  }

  ngOnInit() {
    this.movieId = Number(this.route.snapshot.paramMap.get('id'));
    this.movieService.getMovie(this.movieId).subscribe((data: Movie) => {
      this.movie = data;
    });
    
  }

  goBack() {
    $("#deleteModal, #goBackModal").modal('hide');
    this.router.navigate(['']);
  }

  editMovie() {
    this.router.navigate(['EditMovie/' + this.movie.id]);
  }

  deleteMovie() {
    $("#deleteModal").modal('show');
  }

  cancelDelete() {
    $("#deleteModal").modal('hide');
  }

  confirmDelete() {
    this.deleting = true;
    this.movieService.deleteMovie(this.movie.id).subscribe(() => {
      $("#deleteModal").modal('hide');
      $("#goBackModal").modal('show');
    }, error => {
        this.deleting = false;
        alert(error);
    });
  }
}
