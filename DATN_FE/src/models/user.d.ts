interface IUser {
  id: string;
  username: string;
  isActive: boolean;
  role: IRole;
  name?: string;
  email?: string;
  phoneNumber?: string;
  address?: string;
  createdAt?: string;
  modifiedAt?: string;
}

interface IRole {
  id: string;
  roleName: string;
}
