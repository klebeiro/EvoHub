export interface Repository {
  id: number;
  name: string;
  fullName: string;
  description?: string; // tornando o campo opcional
  url: string;
  updatedAt: string; // ou Date, dependendo de como você pretende manipular a data
  homepage?: string; // tornando o campo opcional
  language?: string;
}
