import { Genre } from './Genre';

export interface User {
  id: number;
  name: string;
  nick: string;
  aboutMe: string;
  gener: Genre;
  password: string;
  cpf: string;
  birth: Date;
  telephone: string;
  cellPhone: string;
  email: string;
  image: string;
  receiveCellPhoneMessage: boolean;
  receiveEmailMessage: boolean;
  date: Date;
}
