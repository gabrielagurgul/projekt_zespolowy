//
//  BudgetFetcher.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 10/12/2021.
//

import Foundation

typealias ID = Int
typealias PLN = Double
fileprivate let session: URLSession = URLSession.shared


func getArrayOfBudgetCategories() async throws -> [BudgetType] {
	let urlRequest = try prepareUrlRequest(API.GET.availableCategories, method: .GET)
	let (data,_) = try await session.data(for: urlRequest)
	guard let arrayOfBudgetsCategories = try? JSONDecoder().decode([BudgetType].self, from: data) else {
		throw ApiError.invalidDataDecoding
	}
	return arrayOfBudgetsCategories
}

func getBudgetBy(_ id: ID) async throws -> [Budget.BudgetAPI] {
	let urlRequest = try prepareUrlRequest(API.GET.budgetDescription + "\(id)", method: .GET)
	let (data,_) = try await session.data(for: urlRequest)
	guard let servData = try? JSONDecoder().decode([Budget.BudgetAPI].self, from: data) else {
		throw ApiError.invalidDataDecoding
	}
	return servData
}

func addBudget(_ budget: Budget, category: REST.Category) async throws {
	var urlRequest = try prepareUrlRequest(API.POST.addBudget + "\(category.rawValue)", method: .POST)
	guard let data = try? JSONEncoder().encode(budget.data) else {
		throw ApiError.invalidDataEncoding
	}
	urlRequest.httpBody = data
	_ = try await session.data(for: urlRequest)
}

func addUserSalary(ammount: PLN) async throws -> PLN {
	let urlRequest = try prepareUrlRequest(API.POST.addSalary, method: .POST)
	let (data,_) = try await session.data(for: urlRequest)
	guard let ammount = try? JSONDecoder().decode(PLN.self, from: data) else {
		throw ApiError.invalidDataDecoding
	}
	return ammount
}

func getCash(from request: Money) async throws -> PLN {
	let urlRequest = try prepareUrlRequest(request.endPoint, method: .GET)
	let (data,_) = try await session.data(for: urlRequest)
	guard let ammount = try? JSONDecoder().decode(PLN.self, from: data) else {
		throw ApiError.invalidDataDecoding
	}
	return ammount
}

enum Money {
	case LastSalary
	case Budget
	
	var endPoint: String {
		switch self {
		case .LastSalary:
			return API.GET.lastSebastian
		case .Budget:
			return API.GET.availableCash
		}
	}
}

func getPredictionforType(_ id: ID, budget: Budget.BudgetAPI) async throws -> Double {
	var urlRequest = try prepareUrlRequest(API.PUT.getPredictionByType + "\(id)", method: .PUT)
	guard let data = try? JSONEncoder().encode(budget) else {
		throw ApiError.invalidDataEncoding
	}
	urlRequest.httpBody = data
	let (prediction, _) = try await session.data(for: urlRequest)
	
	guard let prediction = try? JSONDecoder().decode(Double.self, from: prediction) else {
		throw ApiError.invalidDataDecoding
	}
	return prediction
}

enum REST: String {
	case GET = "GET"
	case PUT = "PUT"
	case DELETE = "DELETE"
	case POST = "POST"
	
	enum Category: Int {
		case Food = 1
		case Home = 2
		case Health = 3
		case Family = 4
		case Car = 5
		case Entertainment = 6
		case Stimulant = 7
		case Investment_S = 8
		case Investment_R = 9
		case Month_income = 10
		case Budget = 11
	}
	
	static func getCategoryById(id: ID) -> Category {
		switch id {
		case 1: return .Food
		case 2: return .Home
		case 3: return .Health
		case 4: return .Family
		case 5: return .Car
		case 6: return .Entertainment
		case 7: return .Stimulant
		case 8: return .Investment_S
		case 9: return .Investment_R
		case 10: return .Month_income
		case 11: return .Budget
		default: return .Food
		}
	}
}

enum ApiError: Error, LocalizedError {
	case invalidDataDecoding
	case invalidURL
	case invalidDataEncoding
	var errorDescription: String? {
		switch self {
		case .invalidDataDecoding:
			return "Nie udało się przekonwertować danych otrzymanych od serwera"
		case .invalidURL:
			return "Nie udało się utworzyć adresu url"
		case .invalidDataEncoding:
			return "Nie udało się utworzyć danych do przesłania na serwer"
		}
	}
}

private func prepareUrlRequest(_ apiURL: String, method: REST) throws -> URLRequest {
	guard let url = URL(string: apiURL) else {
		throw ApiError.invalidURL
	}
	var urlRequest = URLRequest(url: url)
	urlRequest.setValue("application/json", forHTTPHeaderField: "Content-Type")
	urlRequest.httpMethod = method.rawValue
	return urlRequest
}
