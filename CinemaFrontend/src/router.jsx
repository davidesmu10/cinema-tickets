import { Routes, Route } from "react-router-dom";
import MoviesPage from "./pages/MoviesPage";
import MovieDetailPage from "./pages/MovieDetailPage";
import SeatSelectionPage from "./pages/SeatSelectionPage";
import CheckoutPage from "./pages/CheckoutPage";
import ConfirmationPage from "./pages/ConfirmationPage";

export default function Router() {
  return (
    <Routes>
      <Route path="/" element={<MoviesPage />} />
      <Route path="/movies/:id" element={<MovieDetailPage />} />
      <Route path="/movies/:id/seats" element={<SeatSelectionPage />} />
      <Route path="/checkout" element={<CheckoutPage />} />
      <Route path="/confirmation/:reservationId" element={<ConfirmationPage />} />
    </Routes>
  );
}
