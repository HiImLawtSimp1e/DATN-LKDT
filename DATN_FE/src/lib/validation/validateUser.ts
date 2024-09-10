export const validateAddUser = (
  username: string,
  password: string,
  name: string,
  email: string,
  phoneNumber: string,
  address: string
): [string[], boolean] => {
  let errors: string[] = [];

  // Kiểm tra tên tài khoản
  if (username.trim().length === 0) {
    errors.push("Tên tài khoản là bắt buộc.");
  } else if (username.trim().length < 6 || username.trim().length > 100) {
    errors.push("Tên tài khoản phải từ 6 đến 100 ký tự.");
  }

  // Kiểm tra mật khẩu
  if (password.trim().length === 0) {
    errors.push("Mật khẩu là bắt buộc.");
  } else if (password.trim().length < 6 || password.trim().length > 100) {
    errors.push("Mật khẩu phải từ 6 đến 100 ký tự.");
  }

  // Kiểm tra họ và tên
  if (name.trim().length === 0) {
    errors.push("Họ và tên là bắt buộc.");
  } else if (name.trim().length < 6 || name.trim().length > 50) {
    errors.push("Họ và tên phải từ 6 đến 50 ký tự.");
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

  // Kiểm tra số điện thoại
  if (phoneNumber.trim().length === 0) {
    errors.push("Số điện thoại là bắt buộc.");
  } else {
    const phoneRegex = /^(\+?\d{1,3})?0?\d{9}$/;
    if (!phoneNumber.match(phoneRegex)) {
      errors.push("Số điện thoại không hợp lệ.");
    }
  }

  // Kiểm tra địa chỉ
  if (address.trim().length === 0) {
    errors.push("Địa chỉ là bắt buộc.");
  } else if (address.trim().length < 6 || address.trim().length > 250) {
    errors.push("Địa chỉ phải từ 6 đến 250 ký tự.");
  }

  return [errors, errors.length === 0];
};

export const validateUpdateUser = (
  name: string,
  email: string,
  phoneNumber: string,
  address: string
): [string[], boolean] => {
  let errors: string[] = [];

  // Kiểm tra họ và tên
  if (name.trim().length === 0) {
    errors.push("Họ và tên là bắt buộc.");
  } else if (name.trim().length < 6 || name.trim().length > 50) {
    errors.push("Họ và tên phải từ 6 đến 50 ký tự.");
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

  // Kiểm tra số điện thoại
  if (phoneNumber.trim().length === 0) {
    errors.push("Số điện thoại là bắt buộc.");
  } else {
    const phoneRegex = /^(\+?\d{1,3})?0?\d{9}$/;
    if (!phoneNumber.match(phoneRegex)) {
      errors.push("Số điện thoại không hợp lệ.");
    }
  }

  // Kiểm tra địa chỉ
  if (address.trim().length === 0) {
    errors.push("Địa chỉ là bắt buộc.");
  } else if (address.trim().length < 6 || address.trim().length > 250) {
    errors.push("Địa chỉ phải từ 6 đến 250 ký tự.");
  }

  return [errors, errors.length === 0];
};
