import Link from "next/link";

interface IProps {
  address: IAddress;
}

const ShoppingCartAddress = ({ address }: IProps) => {
  return (
    <div className="mb-6 rounded-lg bg-white p-6 shadow-md">
      <div className="px-4 py-5 sm:px-6 flex justify-end">
        <Link
          href={`/profile`}
          className="text-lg leading-6 font-medium text-blue-600"
        >
          Sửa
        </Link>
      </div>
      <div className="border-t border-gray-200 px-4 py-5 sm:p-0">
        <dl className="sm:divide-y sm:divide-gray-200">
          <div className="py-5 px-6 grid grid-cols-3 gap-4">
            <dt className="text-sm font-medium text-gray-500">Họ và tên</dt>
            <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
              {address.name}
            </dd>
          </div>
          <div className="py-5 px-6 grid grid-cols-3 gap-4">
            <dt className="text-sm font-medium text-gray-500">Email</dt>
            <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
              {address.email}
            </dd>
          </div>
          <div className="py-5 px-6 grid grid-cols-3 gap-4">
            <dt className="text-sm font-medium text-gray-500">Số điện thoại</dt>
            <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
              {address.phoneNumber}
            </dd>
          </div>
          <div className="py-5 px-6 grid grid-cols-3 gap-4">
            <dt className="text-sm font-medium text-gray-500">Địa chỉ</dt>
            <dd className="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
              {address.address}
            </dd>
          </div>
        </dl>
      </div>
    </div>
  );
};

export default ShoppingCartAddress;
