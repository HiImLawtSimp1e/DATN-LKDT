"use server";

import { validateLogin, validateRegister } from "@/lib/validation/validateAuth";
import { cookies as nextCookies } from "next/headers";

interface LoginFormData {
  username: string;
  password: string;
}

interface RegisterFormData {
  username: string;
  password: string;
  confirmPassword: string;
  name: string;
  email: string;
  phoneNumber: string;
  address: string;
}

export const customerLoginAction = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  //get value from formData
  const username = formData.get("username") as string;
  const password = formData.get("password") as string;
  const loginData: LoginFormData = { username, password };

  //fetch api [POST] /Auth/login
  const res = await fetch("http://localhost:5000/api/Auth/login", {
    method: "POST",
    body: JSON.stringify(loginData),
    headers: { "Content-Type": "application/json" },
  });

  //client validation
  const [errors, isValid] = validateLogin(username, password);

  if (!isValid) {
    //console.log(errors);
    return { errors };
  }

  const responseData: ApiResponse<string> = await res.json();
  // console.log(responseData);
  const { data, success, message } = responseData;

  if (success) {
    // If the response is success, revalidate the path and redirect
    const cookieStore = nextCookies();
    cookieStore.set("authToken", data);
    return { success: true, errors: [], data };
  } else {
    return { errors: [message] };
  }
};

export const adminLoginAction = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  //get value from formData
  const username = formData.get("username") as string;
  const password = formData.get("password") as string;

  //client validation
  const [errors, isValid] = validateLogin(username, password);

  if (!isValid) {
    //console.log(errors);
    return { errors };
  }

  const loginData: LoginFormData = { username, password };

  //fetch api [POST] /Auth/admin/login
  const res = await fetch("http://localhost:5000/api/Auth/admin/login", {
    method: "POST",
    body: JSON.stringify(loginData),
    headers: { "Content-Type": "application/json" },
  });

  const responseData: ApiResponse<string> = await res.json();
  // console.log(responseData);
  const { data, success, message } = responseData;

  if (success) {
    // If the response is success, revalidate the path and redirect
    const cookieStore = nextCookies();
    cookieStore.set("authToken", data);
    return { success: true, errors: [], data };
  } else {
    return { errors: [message] };
  }
};

export const registerAction = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  //get value from formData
  const username = formData.get("username") as string;
  const password = formData.get("password") as string;
  const confirmPassword = formData.get("confirmPassword") as string;
  const name = formData.get("name") as string;
  const email = formData.get("email") as string;
  const phoneNumber = formData.get("phoneNumber") as string;
  const address = formData.get("address") as string;

  //client validation
  const [errors, isValid] = validateRegister(
    username,
    password,
    confirmPassword,
    name,
    email,
    phoneNumber,
    address
  );

  if (!isValid) {
    //console.log(errors);
    return { errors };
  }

  const registerData: RegisterFormData = {
    username,
    password,
    confirmPassword,
    name,
    email,
    phoneNumber,
    address,
  };

  //fetch api [POST] /Auth/register
  const res = await fetch("http://localhost:5000/api/Auth/register", {
    method: "POST",
    body: JSON.stringify(registerData),
    headers: { "Content-Type": "application/json" },
  });

  const responseData: ApiResponse<string> = await res.json();
  // console.log(responseData);
  const { data, success, message } = responseData;

  if (success) {
    // If the response is success, revalidate the path and redirect
    const cookieStore = nextCookies();
    cookieStore.set("authToken", data);
    return { success: true, errors: [], data };
  } else {
    return { errors: [message] };
  }
};
