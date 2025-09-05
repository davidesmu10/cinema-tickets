import { Link } from "react-router-dom";

export default function Navbar() {
  return (
    <nav className="bg-gray-900 text-white p-4">
      <div className="container mx-auto flex justify-between">
        <Link to="/" className="font-bold text-xl">🎬 CinemaApp</Link>
      </div>
    </nav>
  );
}
