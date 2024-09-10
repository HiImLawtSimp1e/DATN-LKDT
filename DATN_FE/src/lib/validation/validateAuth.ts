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
  phoneNumber: string,
  address: string
): [string[], boolean] => {
  const errors: string[] = [];

  if (!username || username.trim().length === 0) {
    errors.push("Bạn chưa nhập tên tài khoản");
  } else if (username.length < 6 || username.length > 100) {
    errors.push("Tên tài phải dài hơn 6 ký tự & ngắn hơn 100 ký tự");
  }

  if (!password || password.trim().length === 0) {
    errors.push("Mật khẩu không được để trống");
  } else if (password.length < 6 || password.length > 100) {
    errors.push("Mật khẩu không được dài hơn 100 ký tự & ngắn hơn 6 ký tự");
  }

  if (password !== confirmPassword) {
    errors.push("Xác nhận mật khẩu không khớp");
  }

  if (!name || name.trim().length === 0) {
    errors.push("Bạn chưa nhập họ và tên");
  } else if (name.length < 6 || name.length > 50) {
    errors.push("Họ tên không được dài hơn 50 ký tự & ngắn hơn 6 ký tự");
  }

  // Kiểm tra email
  if (email.trim().length === 0) {
    errors.push("Email là bắt buộc.");
  } else if (email.trim().length < 6 || email.trim().length > 30) {
    errors.push("Email phải từ 6 đến 30 ký tự.");
  } else {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!email.match(emailRegex)) {
      errors.push("Email không hợp lệ.");
    }
  }

  if (!phoneNumber || phoneNumber.trim().length === 0) {
    errors.push("Bạn chưa nhập số điện thoại");
  } else if (!/^(\+?\d{1,3})?0?\d{9}$/.test(phoneNumber)) {
    errors.push("Số điện thoại không hợp lệ");
  }

  if (!address || address.trim().length === 0) {
    errors.push("Bạn chưa nhập địa chỉ");
  } else if (address.length < 6 || address.length > 250) {
    errors.push("Địa chỉ không được dài hơn 250 ký tự & ngắn hơn 6 ký tự");
  }

  return [errors, errors.length === 0];
};
