import Image from "next/image";
import TagFiled from "../ui/tag";

const Transactions = () => {
  return (
    <div className="my-4 bg-[#182237] p-5 rounded-lg">
      <h2 className="mb-5 text-gray-400 text-lg font-light">
        Các Giao Dịch Mới Nhất
      </h2>

      <div className="relative overflow-x-auto">
        <table className="w-full text-left text-gray-400">
          <thead className="text-md text-gray-400">
            <tr>
              <th scope="col" className="px-6 py-3">
                Tên
              </th>
              <th scope="col" className="px-6 py-3">
                Trạng Thái
              </th>
              <th scope="col" className="px-6 py-3">
                Ngày
              </th>
              <th scope="col" className="px-6 py-3">
                Số Tiền
              </th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td className="px-6 py-4 ">
                <div className="flex items-center gap-2">
                  <Image
                    src="/noavatar.png"
                    alt=""
                    width={40}
                    height={40}
                    className="rounded-full"
                  />
                  <span className="text-white">John Doe</span>
                </div>
              </td>
              <td className="px-6 py-4">
                <TagFiled cssClass="bg-yellow-500" context="Chờ Xử Lý" />
              </td>
              <td className="px-6 py-4">14.02.2024</td>
              <td className="px-6 py-4">3.200 VNĐ</td>
            </tr>
            <tr>
              <td className="px-6 py-4 ">
                <div className="flex items-center gap-2">
                  <Image
                    src="/noavatar.png"
                    alt=""
                    width={40}
                    height={40}
                    className="rounded-full"
                  />
                  <span className="text-white">John Doe</span>
                </div>
              </td>
              <td className="px-6 py-4">
                <TagFiled cssClass="bg-yellow-500" context="Chờ Xử Lý" />
              </td>
              <td className="px-6 py-4">14.02.2024</td>
              <td className="px-6 py-4">3.200 VNĐ</td>
            </tr>
            <tr>
              <td className="px-6 py-4 ">
                <div className="flex items-center gap-2">
                  <Image
                    src="/noavatar.png"
                    alt=""
                    width={40}
                    height={40}
                    className="rounded-full"
                  />
                  <span className="text-white">John Doe</span>
                </div>
              </td>
              <td className="px-6 py-4">
                <TagFiled cssClass="bg-yellow-500" context="Chờ Xử Lý" />
              </td>
              <td className="px-6 py-4">14.02.2024</td>
              <td className="px-6 py-4">3.200 VNĐ</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default Transactions;
