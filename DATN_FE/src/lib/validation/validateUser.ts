export const validateAddUser = (
  accountName: string,
  password: string,
  fullName: string,
  email: string,
  phone: string,
  address: string
): [string[], boolean] => {
  let errors: string[] = [];

  // Kiểm tra tên tài khoản
  const accountNameError = validateRequired(accountName, "Tên tài khoản");
  if (accountNameError) errors.push(accountNameError);
  else {
    const accountNameError = validateLength(
      accountName,
      "Tên tài khoản",
      6,
      100
    );
    if (accountNameError) errors.push(accountNameError);
  }

  // Kiểm tra mật khẩu
  const passwordRequiredError = validateRequired(password, "Mật khẩu");
  if (passwordRequiredError) errors.push(passwordRequiredError);
  else {
    const passwordLengthError = validateLength(password, "Mật khẩu", 6, 100);
    if (passwordLengthError) errors.push(passwordLengthError);
  }

  // Kiểm tra họ và tên đầy đủ
  const fullNameRequiredError = validateRequired(fullName, "Họ và tên");
  if (fullNameRequiredError) errors.push(fullNameRequiredError);
  else {
    const fullNameLengthError = validateLength(fullName, "Họ và tên", 6, 50);
    if (fullNameLengthError) errors.push(fullNameLengthError);
  }

  // Kiểm tra email
  const emailRequiredError = validateRequired(email, "Email");
  if (emailRequiredError) errors.push(emailRequiredError);
  else {
    const emailError = validateEmail(email);
    if (emailError) errors.push(emailError);
  }

  // Kiểm tra số điện thoại
  const phoneRequiredError = validateRequired(phone, "Số điện thoại");
  if (phoneRequiredError) errors.push(phoneRequiredError);
  else {
    const phoneError = validatePhone(phone);
    if (phoneError) errors.push(phoneError);
  }

  // Kiểm tra địa chỉ
  const addressRequiredError = validateRequired(address, "Địa chỉ");
  if (addressRequiredError) errors.push(addressRequiredError);
  else {
    const addressLengthError = validateLength(address, "Địa chỉ", 6, 250);
    if (addressLengthError) errors.push(addressLengthError);
  }

  return [errors, errors.length === 0];
};

export const validateUpdateUser = (
  fullName: string,
  email: string,
  phone: string,
  address: string
): [string[], boolean] => {
  let errors: string[] = [];

  // Kiểm tra họ và tên đầy đủ (tùy chọn)
  if (fullName.trim().length > 0) {
    const fullNameLengthError = validateLength(fullName, "Họ và tên", 6, 50);
    if (fullNameLengthError) errors.push(fullNameLengthError);
  }

  // Kiểm tra email
  const emailRequiredError = validateRequired(email, "Email");
  if (emailRequiredError) errors.push(emailRequiredError);
  else {
    const emailError = validateEmail(email);
    if (emailError) errors.push(emailError);
  }

  // Kiểm tra số điện thoại
  const phoneRequiredError = validateRequired(phone, "Số điện thoại");
  if (phoneRequiredError) errors.push(phoneRequiredError);
  else {
    const phoneError = validatePhone(phone);
    if (phoneError) errors.push(phoneError);
  }

  // Kiểm tra địa chỉ
  const addressRequiredError = validateRequired(address, "Địa chỉ");
  if (addressRequiredError) errors.push(addressRequiredError);
  else {
    const addressLengthError = validateLength(address, "Địa chỉ", 6, 250);
    if (addressLengthError) errors.push(addressLengthError);
  }

  return [errors, errors.length === 0];
};

const validateRequired = (value: string, fieldName: string): string | null => {
  return value.trim().length === 0 ? `${fieldName} là bắt buộc.` : null;
};

const validateLength = (
  value: string,
  fieldName: string,
  min: number,
  max: number
): string | null => {
  if (value.trim().length < min || value.trim().length > max) {
    return `${fieldName} phải từ ${min} đến ${max} ký tự.`;
  }
  return null;
};

const validateEmail = (email: string): string | null => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return !email.match(emailRegex) ? "Email không hợp lệ." : null;
};

const validatePhone = (phone: string): string | null => {
  const phoneRegex = /^(\+?\d{1,3})?0?\d{9}$/;
  return !phone.match(phoneRegex) ? "Số điện thoại không hợp lệ." : null;
};
