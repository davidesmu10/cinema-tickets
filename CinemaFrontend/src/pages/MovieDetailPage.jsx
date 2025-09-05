import { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import { getMovieById, getShowtimes } from "../api/api";
import Loader from "../components/Loader";

export default function MovieDetailPage() {
  const { id } = useParams();
  const [movie, setMovie] = useState(null);
  const [showtimes, setShowtimes] = useState([]);

  useEffect(() => {
    getMovieById(id).then(res => setMovie(res.data));
    getShowtimes(id).then(res => setShowtimes(res.data));
  }, [id]);

  if (!movie) return <Loader />;

  return (
    <div className="max-w-3xl mx-auto bg-white shadow p-6 rounded">
      <img src={movie.posterUrl} className="w-full h-96 object-cover rounded" />
      <h1 className="text-2xl font-bold mt-4">{movie.title}</h1>
      <p>{movie.description}</p>
      <h2 className="mt-6 text-lg font-semibold">Horarios disponibles</h2>
      <div className="flex flex-wrap gap-3 mt-2">
        {showtimes.map(st => (
          <Link
            key={st.id}
            to={`/movies/${id}/seats?showtimeId=${st.id}`}
            className="bg-blue-600 text-white px-4 py-2 rounded"
          >
            {new Date(st.startTime).toLocaleString()}
          </Link>
        ))}
      </div>
    </div>
  );
}
