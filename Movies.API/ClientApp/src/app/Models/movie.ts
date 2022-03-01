import { Genre } from "./genre";

export class Movie {
  id: number;
  title: string;
  overview: string;
  releaseDate: Date;
  rating: number;
  language: string;
  genres: Genre[];

  constructor() {

  }
}
