import { Link } from "react-router-dom";

export default function MovieCard({ movie }) {
  return (
    <div className="bg-white shadow rounded-lg p-4">
      <img src={movie.posterUrl} alt={movie.title} className="rounded w-full h-64 object-cover" />
      <h2 className="mt-2 text-lg font-bold">{movie.title}</h2>
      <p className="text-gray-600">{movie.genre}</p>
      <Link to={`/movies/${movie.id}`} className="mt-2 inline-block bg-blue-600 text-white px-4 py-2 rounded">
        Ver detalles
      </Link>
    </div>
  );
}
