import Link from "next/link";

interface IProps {
  title?: string;
  content?: string;
}

const AdminNotFoundPage = ({ title, content }: IProps) => {
  return (
    <main className="grid min-h-full place-items-center px-6 py-24 sm:py-32 lg:px-8">
      <div className="text-center">
        <h1 className="text-5xl font-bold text-indigo-400">404</h1>
        <p className="mt-4 text-3xl font-bold tracking-tight text-gray-300">
          {title}
        </p>
        <p className="mt-6 text-base leading-7 text-gray-400">{content}</p>
        <div className="mt-10 flex items-center justify-center gap-x-6">
          <Link
            href="/dashboard"
            className="rounded-md bg-indigo-600 px-3.5 py-2.5 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
          >
            Quay về trang chủ
          </Link>
        </div>
      </div>
    </main>
  );
};

export default AdminNotFoundPage;
