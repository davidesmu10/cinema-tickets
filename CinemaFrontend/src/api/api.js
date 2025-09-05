import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE,
});

export const getMovies = () => api.get("/movies");
export const getMovieById = (id) => api.get(`/movies/${id}`);
export const getShowtimes = (id) => api.get(`/movies/${id}/showtimes`);
export const getSeats = (screeningId) =>
  api.get(`/movies/${screeningId}/seats`);

export const reserveSeats = (payload) => api.post("/reservations", payload);

export default api;
