"use client";

import { updateVoucher } from "@/action/voucherAction";
import DatePickerField from "@/components/ui/date-picke/date-picker";
import InputField from "@/components/ui/input";
import SelectField from "@/components/ui/select";
import { useCustomActionState } from "@/lib/custom/customHook";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";

interface IProps {
  voucher: IVoucherItem;
}

const UpdateVoucherForm = ({ voucher }: IProps) => {
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    updateVoucher,
    initialState
  );

  const [toastDisplayed, setToastDisplayed] = useState(false);

  const [formData, setFormData] = useState<IVoucherItem>(voucher);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    formAction(formData);
    setToastDisplayed(false); // Reset toastDisplayed when submitting
  };

  const handleChange = (
    e:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setFormData((prevFormData) => ({
      ...prevFormData,
      [name]: value,
    }));
  };

  useEffect(() => {
    if (formState.errors.length > 0 && !toastDisplayed) {
      toast.error("Cập nhật voucher thất bại");
      setToastDisplayed(true); // Set toastDisplayed to true to prevent multiple toasts
    }
    if (formState.success) {
      toast.success("Cập nhật voucher thành công");
      router.push(`/dashboard/vouchers`);
    }
  }, [formState, toastDisplayed]);

  return (
    <form onSubmit={handleSubmit} className="px-4 w-full">
      <input type="hidden" name="id" value={voucher.id} />
      <InputField
        label="Mã giảm giá"
        id="code"
        name="code"
        value={formData.code}
        onChange={handleChange}
        readonly
      />
      <InputField
        label="Tên voucher"
        id="voucherName"
        name="voucherName"
        value={formData.voucherName}
        onChange={handleChange}
        required
      />
      <SelectField
        label="Loại giảm giá"
        id="isDiscountPercent"
        name="isDiscountPercent"
        value={formData.isDiscountPercent.toString()}
        onChange={handleChange}
        options={[
          { label: "Phần trăm", value: "true" },
          { label: "Cố định", value: "false" },
        ]}
      />
      <InputField
        type="number"
        label="Giá trị giảm giá"
        id="discountValue"
        name="discountValue"
        value={formData.discountValue.toString()}
        onChange={handleChange}
        min-value={0}
        required
      />
      <InputField
        type="number"
        label="Điều kiện áp dụng(với đơn hàng có giá trị từ)"
        id="minOrderCondition"
        name="minOrderCondition"
        value={formData.minOrderCondition.toString()}
        onChange={handleChange}
        min-value={0}
      />
      {formData.isDiscountPercent.toString() === "true" && (
        <InputField
          type="number"
          label="Số tiền giảm tối đa(đối với voucher giảm giá theo %)"
          id="maxDiscountValue"
          name="maxDiscountValue"
          value={formData.maxDiscountValue.toString()}
          onChange={handleChange}
          min-value={0}
        />
      )}
      <InputField
        type="number"
        label="Số lượng voucher"
        id="voucherQuantity"
        name="voucherQuantity"
        value={formData.quantity.toString()}
        onChange={handleChange}
        min-value={0}
      />
      <DatePickerField
        label="Ngày bắt đầu"
        id="startDate"
        name="startDate"
        value={formData.startDate}
        onChange={handleChange}
      />

      <DatePickerField
        label="Ngày kết thúc"
        id="endDate"
        name="endDate"
        value={formData.endDate}
        onChange={handleChange}
      />
      <SelectField
        label="Trạng thái"
        id="isActive"
        name="isActive"
        value={formData.isActive.toString()}
        onChange={handleChange}
        options={[
          { label: "Hoạt động", value: "true" },
          { label: "Không hoạt động", value: "false" },
        ]}
      />
      <button
        type="submit"
        className="float-right mt-4 text-white bg-blue-700 hover:bg-blue-800 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center"
      >
        Cập nhật Voucher
      </button>
      {formState.errors.length > 0 && (
        <ul>
          {formState.errors.map((error, index) => (
            <li className="text-red-400" key={index}>
              {error}
            </li>
          ))}
        </ul>
      )}
    </form>
  );
};

export default UpdateVoucherForm;
