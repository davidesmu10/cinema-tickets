import { useEffect, useState } from "react";
import { getMovies } from "../api/api";
import MovieCard from "../components/MovieCard";
import Loader from "../components/Loader";

export default function MoviesPage() {
  const [movies, setMovies] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getMovies().then(res => {
      setMovies(res.data);
      setLoading(false);
    });
  }, []);

  if (loading) return <Loader />;

  return (
    <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
      {movies.map(m => <MovieCard key={m.id} movie={m} />)}
    </div>
  );
}
