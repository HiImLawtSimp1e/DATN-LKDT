export const validateLogin = (
  username: string,
  password: string
): [string[], boolean] => {
  const errors: string[] = [];

  if (!username || username.trim().length === 0) {
    errors.push("Tên tài khoản không được để trống");
  }

  if (!password || password.trim().length === 0) {
    errors.push("Mật khẩu không được để trống");
  }

  return [errors, errors.length === 0];
};

export const validateRegister = (
  username: string,
  password: string,
  confirmPassword: string,
  name: string,
  email: string,
  phoneNumber: string
): [string[], boolean] => {
  const errors: string[] = [];

  if (!username || username.trim().length === 0) {
    errors.push("Tên tài khoản không được để trống");
  }

  if (!password || password.trim().length === 0) {
    errors.push("Password is required.");
  } else if (password.length < 6 || password.length > 100) {
    errors.push("Mật khẩu không được dài hơn 100 ký tự & ngắn hơn 6 ký tự");
  }

  if (password !== confirmPassword) {
    errors.push("Xác nhận mật khẩu không khớp");
  }

  if (!name || name.trim().length === 0) {
    errors.push("Bạn chưa nhập họ và tên");
  } else if (name.length < 6) {
    errors.push("Họ tên không được ngắn hơn 6 ký tự");
  } else if (name.length > 50) {
    errors.push("Họ tên không được dài hơn 50 ký tự");
  }

  if (!email || email.trim().length === 0) {
    errors.push("Bạn chưa nhập Email");
  } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
    errors.push("Email không hợp lệ");
  }

  if (!phoneNumber || phoneNumber.trim().length === 0) {
    errors.push("Bạn chưa nhập số điện thoại");
  } else if (!/^(\+?\d{1,3})?0?\d{9}$/.test(phoneNumber)) {
    errors.push("Số điện thoại không hợp lệ");
  }

  return [errors, errors.length === 0];
};
