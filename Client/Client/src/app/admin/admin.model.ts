
export interface IUserModel {
  email: string;
  fullName: string;
  id: string;
  joinDate: string;
  roles: string[];
}

export interface IUserRole {
  id: string;
  name: string;
}
